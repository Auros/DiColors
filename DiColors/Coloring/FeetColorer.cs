using SiraUtil.Logging;
using System.Linq;
using UnityEngine;
using Zenject;

namespace DiColors.Coloring;

internal class FeetColorer : Colorizer
{
    public override string Name => "Menu Feet";

    private readonly SpriteRenderer? _feetSprite;

    public FeetColorer(SiraLog siraLog, SceneContext sceneContext)
    {
        var wrapper = sceneContext.gameObject.scene.GetRootGameObjects().Where(g => g.name == "Wrapper").FirstOrDefault();
        if (wrapper != null)
        {
            var feet = wrapper.transform.Find("MenuEnvironmentCore/PlayersPlace/Feet");
            if (feet != null)
                _feetSprite = feet.GetComponent<SpriteRenderer>();
        }


        if (_feetSprite == null)
            siraLog.Error("Could not find the feet sprite!");
    }

    public override Color Color
    {
        get => _config.MenuFeetColor;
        set
        {
            _config.MenuFeetColor = value;
            SetColor(_config.MenuFeetColor);
        }
    }

    private void SetColor(Color color)
    {
        if (_feetSprite != null)
            _feetSprite.color = color;
    }
}
