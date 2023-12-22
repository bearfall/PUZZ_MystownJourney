using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[Serializable]
public class RPGSkode_DialogueClip : PlayableAsset, ITimelineClipAsset
{
    public RPGSkode_DialogueBehaviour template = new RPGSkode_DialogueBehaviour();

    public ClipCaps clipCaps
    {
        get { return ClipCaps.None; }
    }

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<RPGSkode_DialogueBehaviour>.Create(graph, template);

        return playable;
    }
}
