using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Match3.Core
{
    public class DIInstaller : MonoInstaller
    {
        [SerializeField]private GameObject inputTilePrefab;
        [SerializeField]private GameObject playTilePrefab;

        public override void InstallBindings()
        {
            Container.Bind<MatchChecker>().AsCached();
            Container.Bind<ColumnSorter>().AsCached();
            Container.Bind<BoardManager>().FromComponentInHierarchy().AsCached();
            Container.Bind<BoardData>().AsCached();
            Container.Bind<Blaster>().AsCached();
            Container.Bind<PlayTilePool>().FromComponentInHierarchy().AsCached();

            Container.BindFactory<InputTile, InputTileFactory>().FromComponentInNewPrefab(inputTilePrefab);
            Container.BindFactory<PlayTile, PlayTileFactory>().FromComponentInNewPrefab(playTilePrefab);

            Container.Bind<BoardManager>().FromComponentInHierarchy().AsCached().WhenInjectedInto<InputTile>();
            Container.Bind<SpriteRenderer>().FromComponentSibling().AsTransient();
            Container.Bind<InputTile>().FromComponentSibling().AsTransient();

        }

    }
}

