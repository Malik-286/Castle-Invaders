﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEditor;
using UnityEditor.Animations;


#pragma warning disable IDE0005
using Serilog = Meryel.UnityCodeAssist.Serilog;
#pragma warning restore IDE0005


#nullable enable


namespace Meryel.UnityCodeAssist.Editor
{
    internal static partial class UnityClassExtensions
    {
        static GameObject? GetParentGO(GameObject go)
        {
            if (!go)
                return null;

            var parentTransform = go.transform.parent;

            if (parentTransform && parentTransform.gameObject)
                return parentTransform.gameObject;
            else
                return null;
        }

        static string GetId(UnityEngine.Object? obj)
        {
            try
            {
                // obj can be null

                var globalObjectId = GlobalObjectId.GetGlobalObjectIdSlow(obj);
                var objectGuid = globalObjectId.ToString();
                return objectGuid;
            }
            catch (Exception ex)
            {
                // OnBeforeSerialize of user scripts may raise exception
                Serilog.Log.Warning(ex, "GetGlobalObjectIdSlow failed for obj {Obj}", obj);
                return "GlobalObjectId_V1-0-00000000000000000000000000000000-0-0";
            }
        }

        public static Synchronizer.Model.Component_Animation? ToSyncModelOfComponentAnimation(this GameObject go)
        {
            if (!go.TryGetComponent<Animation>(out var animation))
                return null;

            if (!animation.isActiveAndEnabled)
                return null;

            var data = new Synchronizer.Model.Component_Animation();

            data.GameObjectId = GetId(go);

            /*
            var clips = AnimationUtility.GetAnimationClips(go);
            data.Clips = new string[clips.Length];
            for (int i = 0; i < clips.Length; i++)
            {
                data.Clips[i] = clips[i].name;
            }
            */

            var states = new List<string>();
            foreach (AnimationState state in animation)
            {
                states.Add(state.name);
            }
            data.States = states.ToArray();

            return data;
        }

        public static Synchronizer.Model.Component_Animator? ToSyncModelOfComponentAnimator(this GameObject go)
        {
            if (!go.TryGetComponent<Animator>(out var animator))
                return null;

            if (!animator.isActiveAndEnabled)
                return null;

            if (!animator.runtimeAnimatorController)
                return null;

            var data = new Synchronizer.Model.Component_Animator();

            data.GameObjectId = GetId(go);

            var layerCount = animator.layerCount;
            data.LayerIndices = new string[layerCount];
            data.LayerNames = new string[layerCount];
            for (int i = 0; i < layerCount; i++)
            {
                data.LayerIndices[i] = i.ToString();
                data.LayerNames[i] = animator.GetLayerName(i);
            }

            var parameterCount = animator.parameterCount;
            data.ParameterIndices = new string[parameterCount];
            data.ParameterNames = new string[parameterCount];
            data.ParameterHashes = new string[parameterCount];
            data.ParameterTypes = new int[parameterCount];
            for (var i = 0; i < parameterCount; i++)
            {
                var parameter = animator.GetParameter(i);
                data.ParameterIndices[i] = i.ToString();
                data.ParameterNames[i] = parameter.name;
                data.ParameterHashes[i] = parameter.nameHash.ToString();
                data.ParameterTypes[i] = (int)parameter.type;
            }

            // When you specify a state name, or the string used to generate a hash, it should include the name of the parent layer. For example, if you have a Bounce state in the Base Layer, the name is Base Layer.Bounce
            // The name should be in the form Layer.Name or Layer.SubStateMachine.Name
            if (!GetAnimatorStateInfo(animator, out var states, out var transitions) ||
                states == null || transitions == null) //for nullables
                return data;

            var stateCount = states.Count;
            data.StateNames = new string[stateCount];
            data.StateNameHashes = new string[stateCount];
            data.StateTags = new string[stateCount];
            data.StateTagHashes = new string[stateCount];
            data.StateFullPaths = new string[stateCount];
            data.StateFullPathHashes = new string[stateCount];
            for (int i = 0; i < stateCount; i++)
            {
                var state = states[i].state;
                var fullPath = states[i].fullPath;
                data.StateNames[i] = state.name;
                data.StateNameHashes[i] = state.nameHash.ToString();
                data.StateTags[i] = state.tag;
                data.StateTagHashes[i] = Animator.StringToHash(state.tag).ToString();
                data.StateFullPaths[i] = fullPath;
                data.StateFullPathHashes[i] = Animator.StringToHash(fullPath).ToString();
            }

            var transitionCount = transitions.Count;
            data.TransitionNames = new string[transitionCount];
            data.TransitionNameHashes = new string[transitionCount];
            data.TransitionUsernames = new string[transitionCount];
            data.TransitionUsernameHashes = new string[transitionCount];
            data.TransitionFullPaths = new string[transitionCount];
            data.TransitionFullPathHashes = new string[transitionCount];
            for (int i = 0; i < transitionCount; i++)
            {
                var transition = transitions[i].transition;
                var fullPath = transitions[i].fullPath;
                data.TransitionNames[i] = transition.name;
                data.TransitionNameHashes[i] = Animator.StringToHash(transition.name).ToString();
                data.TransitionUsernames[i] = transition.GetDisplayName(transition.destinationState);
                data.TransitionUsernameHashes[i] = Animator.StringToHash(data.TransitionUsernames[i]).ToString();
                data.TransitionFullPaths[i] = fullPath;
                data.TransitionFullPathHashes[i] = Animator.StringToHash(fullPath).ToString();
            }

            var clips = animator.runtimeAnimatorController.animationClips;
            data.Clips = new string[clips.Length];
            for (int i = 0; i < clips.Length; i++)
                data.Clips[i] = clips[i].name;

            return data;

            //var events = clips.SelectMany(c => c.events);
        }

        

        public static bool GetAnimatorStateInfo(Animator animator, out List<(AnimatorState state, string fullPath)>? states, out List<(AnimatorTransition transition, string fullPath)>? transitions)
        {
            AnimatorController? controller = animator.runtimeAnimatorController as AnimatorController;
            if (!controller || controller == null)
            {
                states = null;
                transitions = null;
                return false;
            }

            AnimatorControllerLayer[] layers = controller.layers;
            states = new List<(AnimatorState, string)>();
            transitions = new List<(AnimatorTransition, string)>();
            foreach (AnimatorControllerLayer layer in layers)
            {
                ChildAnimatorState[] animStates = layer.stateMachine.states;
                getStateMachineInfo(layer.stateMachine, 0, layer.name, states, transitions);
            }
            return true;


            static void getStateMachineInfo(AnimatorStateMachine stateMachine, int depth, string curPath,
                List<(AnimatorState state, string fullPath)> states,
                List<(AnimatorTransition transition, string fullPath)> transitions)
            {
                // for performance
                if (depth > 4 || states.Count > 128)
                    return;

                states.AddRange(stateMachine.states.Select(s => (s.state, curPath + "." + s.state.name)));

                //var transitions = stateMachine.GetStateMachineTransitions(stateMachine);
                transitions.AddRange(stateMachine.GetStateMachineTransitions(stateMachine).Select(t => (t, curPath + "." + t.name)));

                foreach (var subStateMachine in stateMachine.stateMachines)
                    getStateMachineInfo(subStateMachine.stateMachine, depth + 1, curPath + "." + subStateMachine.stateMachine.name, states, transitions);
            }
        }

        internal static Synchronizer.Model.GameObject? ToSyncModel(this GameObject go, int priority = 0)
        {
            if (!go)
                return null;

            var data = new Synchronizer.Model.GameObject()
            {
                Id = GetId(go),

                Name = go.name,
                Layer = go.layer.ToString(),
                Tag = go.tag,
                Scene = go.scene.name,

                ParentId = GetId(GetParentGO(go)),
                ChildrenIds = getChildrenIds(go),

                Components = getComponents(go),

                Priority = priority,
            };
            return data;

            static string[] getChildrenIds(GameObject g)
            {
                var ids = new List<string>();
                var limit = 10;//**--
                foreach (Transform child in g.transform)
                {
                    if (!child || !child.gameObject)
                        continue;

                    ids.Add(GetId(child.gameObject));

                    if (--limit <= 0)
                        break;
                }
                return ids.ToArray();
            }

            //**--limit/10
            static string[] getComponents(GameObject g) =>
              g.GetComponents<Component>().Where(c => c).Select(c => c.GetType().FullName).Take(10).ToArray();
            /*(string[] componentNames, Synchronizer.Model.ComponentData[] componentData) getComponents(GameObject g)
            {
                var components = g.GetComponents<Component>();
                var names = components.Select(c => c.name).ToArray();

                var data = new List<Synchronizer.Model.ComponentData>();
                foreach (var comp in components)
                {
                    var name = comp.name;

                    
                }

                return (names, data.ToArray());
            }*/
        }

        internal static Synchronizer.Model.GameObject[]? ToSyncModelOfHierarchy(this GameObject go)
        {
            if (!go)
                return null;

            var list = new List<Synchronizer.Model.GameObject>();

            var parent = GetParentGO(go);
            if (parent != null && parent)
            {
                var parentModel = parent.ToSyncModel();
                if (parentModel != null)
                    list.Add(parentModel);
            }

            int limit = 10;
            foreach (Transform child in go.transform)
            {
                if (!child || !child.gameObject)
                    continue;

                var childModel = child.gameObject.ToSyncModel();
                if (childModel == null)
                    continue;

                list.Add(childModel);

                if (--limit <= 0)
                    break;
            }

            return list.ToArray();
        }

        internal static Synchronizer.Model.ComponentData[]? ToSyncModelOfComponents(this GameObject go)
        {
            if (!go)
                return null;

            var limit = 10;//**--
            return go.GetComponents<Component>().Where(c => c).Select(c => c.ToSyncModel(go)).Where(cd => cd != null).Take(limit).ToArray()!;

            /*
            var components = go.GetComponents<Component>();
            var len = components.Count(c => c != null);
            len = Math.Min(len, limit);//**--limit

            var array = new Synchronizer.Model.ComponentData[len];

            var arrayIndex = 0;
            foreach (var component in components)
            {
                if (component == null)
                    continue;

                array[arrayIndex++] = component.ToSyncModel(go);

                if (arrayIndex >= len)
                    break;
            }

            return array;
            */
        }

        internal static Synchronizer.Model.ComponentData? ToSyncModel(this Component component, GameObject go)
        {
            if (!component || !go)
                return null;

            Type type = component.GetType();
            var list = new List<(string, string)>();
            ShowFieldInfo(type, component, list);

            var data = new Synchronizer.Model.ComponentData()
            {
                GameObjectId = GetId(go),
                Component = component.GetType().FullName,
                Type = Synchronizer.Model.ComponentData.DataType.Component,
                Data = list.ToArray(),
            };
            return data;
        }

        internal static Synchronizer.Model.ComponentData? ToSyncModel(this ScriptableObject so)
        {
            if (!so)
                return null;

            Type type = so.GetType();
            var list = new List<(string, string)>();
            ShowFieldInfo(type, so, list);

            var data = new Synchronizer.Model.ComponentData()
            {
                GameObjectId = GetId(so),
                Component = so.GetType().FullName,
                Type = Synchronizer.Model.ComponentData.DataType.ScriptableObject,
                Data = list.ToArray(),
            };
            return data;
        }


        static bool IsTypeCompatible(Type type)
        {
            if (type == null || !(type.IsSubclassOf(typeof(MonoBehaviour)) || type.IsSubclassOf(typeof(ScriptableObject))))
                return false;
            return true;
        }

        static void ShowFieldInfo(Type type)//, MonoImporter importer, List<string> names, List<Object> objects, ref bool didModify)
        {
            // Only show default properties for types that support it (so far only MonoBehaviour derived types)
            if (!IsTypeCompatible(type))
                return;

            ShowFieldInfo(type.BaseType);//, importer, names, objects, ref didModify);

            FieldInfo[] infos = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly);
            foreach (FieldInfo field in infos)
            {
                if (!field.IsPublic)
                {
                    object[] attr = field.GetCustomAttributes(typeof(SerializeField), true);
                    if (attr == null || attr.Length == 0)
                        continue;
                }

                /*
                if (field.FieldType.IsSubclassOf(typeof(Object)) || field.FieldType == typeof(Object))
                {
                    Object oldTarget = importer.GetDefaultReference(field.Name);
                    Object newTarget = EditorGUILayout.ObjectField(ObjectNames.NicifyVariableName(field.Name), oldTarget, field.FieldType, false);

                    names.Add(field.Name);
                    objects.Add(newTarget);

                    if (oldTarget != newTarget)
                        didModify = true;
                }
                */

                if (field.FieldType.IsValueType && field.FieldType.IsPrimitive && !field.FieldType.IsEnum)
                {

                }
                else if (field.FieldType == typeof(string))
                {

                }
            }
        }

        static void ShowFieldInfo(Type type, UnityEngine.Object unityObjectInstance, List<(string, string)> fields)//, MonoImporter importer, List<string> names, List<Object> objects, ref bool didModify)
        {
            // Only show default properties for types that support it (so far only MonoBehaviour derived types)
            if (!IsTypeCompatible(type))
                return;

            if (!unityObjectInstance)
                return;

            ShowFieldInfo(type.BaseType, unityObjectInstance, fields);//, importer, names, objects, ref didModify);

            FieldInfo[] infos = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly);
            foreach (FieldInfo field in infos)
            {
                if (!field.IsPublic)
                {
                    object[] attr = field.GetCustomAttributes(typeof(SerializeField), true);
                    if (attr == null || attr.Length == 0)
                        continue;
                }

                // check attribute [HideInInspector]
                {
                    object[] attr = field.GetCustomAttributes(typeof(HideInInspector), true);
                    if (attr != null && attr.Length > 0)
                        continue;
                }

                // readonly
                if (field.IsInitOnly)
                    continue;


                /*
                if (field.FieldType.IsSubclassOf(typeof(Object)) || field.FieldType == typeof(Object))
                {
                    Object oldTarget = importer.GetDefaultReference(field.Name);
                    Object newTarget = EditorGUILayout.ObjectField(ObjectNames.NicifyVariableName(field.Name), oldTarget, field.FieldType, false);

                    names.Add(field.Name);
                    objects.Add(newTarget);

                    if (oldTarget != newTarget)
                        didModify = true;
                }
                */

                if (field.FieldType.IsValueType && field.FieldType.IsPrimitive && !field.FieldType.IsEnum)
                {
                    var val = field.GetValue(unityObjectInstance);
                    fields.Add((field.Name, val.ToString()));//**--culture
                }
                else if (field.FieldType == typeof(string))
                {
                    var val = (string)field.GetValue(unityObjectInstance);
                    fields.Add((field.Name, val));
                }
            }
        }

    }
}
