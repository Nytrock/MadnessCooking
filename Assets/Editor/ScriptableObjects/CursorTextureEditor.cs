using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

#if UNITY_EDITOR
[CustomEditor(typeof(CursorTexture), true)]
[CanEditMultipleObjects]
public class CursorTextureEditor : Editor {
    private CursorTexture Cursor { get { return target as CursorTexture; } }

    public override Texture2D RenderStaticPreview(string assetPath, Object[] subAssets, int width, int height) {
        if (Cursor.MainTexture != null) {
            Type t = GetType("UnityEditor.SpriteUtility");
            if (t != null) {
                MethodInfo method = t.GetMethod("RenderStaticPreview", new Type[] { typeof(Sprite), typeof(Color), typeof(int), typeof(int) });
                if (method != null) {
                    Sprite sprite = Sprite.Create(Cursor.MainTexture, new Rect(0, 0, Cursor.MainTexture.width, Cursor.MainTexture.height), new Vector2(Cursor.MainTexture.width / 2, Cursor.MainTexture.height / 2));
                    object ret = method.Invoke("RenderStaticPreview", new object[] { sprite, Color.white, width, height });
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