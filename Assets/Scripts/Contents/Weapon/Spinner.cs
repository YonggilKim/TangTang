using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Spinner : Skill
{
    public int LoopTime { get; set; } = 4; // spinner ������ ���� ����Ÿ�Ӱ���
    public int Delay { get; set; } = 10;

    private void Start()
    {
        ActiveSpinner();
    }
    public void Init()
    {
        Damage = 50f;
    }

    void ActiveSpinner()
    {
        //TODO SOUND

        Sequence EnableSequence = DOTween.Sequence();

        Tween scale = transform.DOScale(1, 2);
        Tween rotate = transform.DORotate(new Vector3(0, 0, -360), LoopTime, RotateMode.FastBeyond360)
            .SetEase(Ease.Linear);//.SetLoops(LoopTime);
        Tween scale2 = transform.DOScale(0, 2);
        Tween rotate2 = transform.DORotate(new Vector3(0, 0, -360), LoopTime, RotateMode.FastBeyond360)
            .SetEase(Ease.Linear);

        EnableSequence.Append(scale).Join(rotate).Append(scale2).Join(rotate2).InsertCallback(Delay, () => ActiveSpinner());
    }


}