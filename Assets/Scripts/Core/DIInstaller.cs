using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Core
{
    public class DIInstaller : MonoInstaller
    {

        public override void InstallBindings()
        {
            Container.Bind<ColumnSorter>().FromComponentInHierarchy().AsCached();
            Container.Bind<MatchChecker>().FromComponentInHierarchy().AsCached();
            Container.Bind<PlayTileFactory>().FromComponentInHierarchy().AsCached();
        }

    }
}

