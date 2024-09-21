using UnityEngine;

public static class SpriteUtility {
    public static Texture2D ToTexture2D(this Sprite sprite) {
        Texture2D texture = new((int)sprite.rect.width, (int)sprite.rect.height);

        Texture2D spriteTexture = sprite.texture;
        Texture2D globalTexture = new(spriteTexture.width, spriteTexture.height, spriteTexture.format, spriteTexture.mipmapCount, false);
        Graphics.CopyTexture(spriteTexture, globalTexture);

        Color[] pixels = globalTexture.GetPixels((int)sprite.textureRect.x,
                                                (int)sprite.textureRect.y,
                                                (int)sprite.textureRect.width,
                                                (int)sprite.textureRect.height);

        texture.SetPixels(pixels);
        texture.Apply();
        return texture;
    }
}
