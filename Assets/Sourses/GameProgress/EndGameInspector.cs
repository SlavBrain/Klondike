using System;
using System.Collections.Generic;

public class EndGameInspector
{
    private List<DumpModel> _dumpModels;

    public EndGameInspector(List<DumpModel> dumpModels)
    {
        _dumpModels = dumpModels;

        SignToEvents();
    }

    public event Action GameEnded;

    private void SignToEvents()
    {
        if (_dumpModels.Count > 0)
        {
            foreach(DumpModel dump in _dumpModels)
            {
                dump.Filled += CheckFillingAllDumps;
            }
        }
    }

    private void CheckFillingAllDumps()
    {
        foreach (DumpModel dump in _dumpModels)
        {
            if (!dump.IsFill)
            {
                return;
            }
        }
        
        StartGameEnd();
    }

    private void StartGameEnd()
    {
        GameEnded?.Invoke();
        PlayerData.Instance.OnSuccessEndedGame();
    }
}
