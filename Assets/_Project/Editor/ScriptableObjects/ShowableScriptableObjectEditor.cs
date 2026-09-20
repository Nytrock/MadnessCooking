using MadnessCooking.General;
using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

#if UNITY_EDITOR
[CustomEditor(typeof(ExtendedScriptableObject), true)]
[CanEditMultipleObjects]
public class ShowableScriptableObjectEditor : Editor {
    private ExtendedScriptableObject Object => target as ExtendedScriptableObject;

    public override Texture2D RenderStaticPreview(string assetPath, Object[] subAssets, int width, int height) {
        if (Object.Icon != null) {
            Type t = GetType("UnityEditor.SpriteUtility");
            if (t != null) {
                MethodInfo method = t.GetMethod("RenderStaticPreview", new Type[] { typeof(Sprite), typeof(Color), typeof(int), typeof(int) });
                if (method != null) {
                    object ret = method.Invoke("RenderStaticPreview", new object[] { Object.Icon, Color.white, width, height });
                    if (ret is Texture2D)
                        return ret as Texture2D;
                }
            }
        }
        return base.RenderStaticPreview(assetPath, subAssets, width, height);
    }

    private static Type GetType(string TypeName) {
        Type type = Type.GetType(TypeName);
        if (type != null)
            return type;

        if (TypeName.Contains(".")) {
            string assemblyName = TypeName.Substring(0, TypeName.IndexOf('.'));
            Assembly assembly = Assembly.Load(assemblyName);
            if (assembly == null)
                return null;
            type = assembly.GetType(TypeName);
            if (type != null)
                return type;
        }

        Assembly currentAssembly = Assembly.GetExecutingAssembly();
        AssemblyName[] referencedAssemblies = currentAssembly.GetReferencedAssemblies();
        foreach (var assemblyName in referencedAssemblies) {
            Assembly assembly = Assembly.Load(assemblyName);
            if (assembly != null) {
                type = assembly.GetType(TypeName);
                if (type != null)
                    return type;
            }
        }
        return null;
    }
}
#endif