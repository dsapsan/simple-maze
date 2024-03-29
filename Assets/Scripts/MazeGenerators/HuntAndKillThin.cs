﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//http://wiki.roblox.com/index.php/Hunt-and-Kill
public class HuntAndKillThin : MazeGenerator<WalledMaze>
{
    public override bool NextStep()
    {
        if (base.NextStep())
            return false;

        maze.SetPass(maze.CurrentCell, true);
        var choices = new List<Vector2Int>();

        foreach (var item in RectangleMaze.Shifts)
        {
            var adj = maze.CurrentCell + item;
            if (maze.InMaze(adj))
                if (!maze.GetPass(adj))
                    choices.Add(adj);
        }

        if (choices.Count == 0)
        {
            for (int i = 0; i < maze.Width; i++)
                for (int n = 0; n < maze.Height; n++)
                {
                    var hunter = new Vector2Int(i, n);
                    if (!maze.GetPass(hunter))
                        foreach (var item in RectangleMaze.Shifts)
                        {
                            var adj = hunter + item;
                            if (maze.InMaze(adj))
                                if (maze.GetPass(adj))
                                {
                                    maze.SetTunnel(hunter, adj, true);
                                    maze.CurrentCell = hunter;
                                    return true;
                                }
                        }
                }
        }
        else
        {
            var index = Random.Range(0, choices.Count);
            maze.SetTunnel(maze.CurrentCell, choices[index], true);
            maze.CurrentCell = choices[index];
            return true;
        }

        return false;
    }
}
