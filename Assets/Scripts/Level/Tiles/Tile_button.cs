using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile_button : Tile
{
    public override byte id { get; protected set; } = 7;

    public override byte[] metadata { set; get; } = { 0, 0, 9, 0 };

    public Tile_button(Vector2Int position) : base(position) { }

    public override void setPowered(bool state)
    {
        if (state)
        {
            powered = true;

            for (int i = 0; i < 4; i++)
            {
                Tile neighbour = getNeighbourByIndex(i);
                if (neighbour == null) continue;
                if (LogicManager.isOutputID(metadata[i]) && !neighbour.getPowered() && LogicManager.isInputID(neighbour.metadata[(i + 2) % 4])) // checks if this tile output at i and neighbour has input at opposite of i
                {
                    neighbour.setPowered(true);
                }
            }
        }
        else
        {
            powered = false;

            tagSelf();

            for (int i = 0; i < 4; i++)
            {
                Tile neighbour = getNeighbourByIndex(i);
                if (neighbour == null) continue;
                if (LogicManager.isOutputID(metadata[i]) && neighbour.getPowered() && LogicManager.isInputID(neighbour.metadata[(i + 2) % 4])) // checks if this tile output at i and neighbour has input at opposite of i
                {
                    neighbour.isConnectedToPowerSource();
                }
            }
        }
    }

    public override bool isConnectedToPowerSource()
    {
        return powered;
    }

    public override void interact()
    {
        setPowered(!powered);
    }
}
