namespace Core
{
    public static class PlayTileFlipper
    {
        public static void Flip(ContainerTile containerTile1, ContainerTile containerTile2)
        {
            if (containerTile1 == null || containerTile2 == null)
                return;

            PlayTile temp = containerTile1.PlayTile;
            containerTile1.SetPlayTile(containerTile2.PlayTile);
            containerTile2.SetPlayTile(temp);
        }
    }
}