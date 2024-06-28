using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public int CurrentWorshipers { get; set; }
    public int WorshipersToSend { get; set; }
    public float Speed { get; set; }
    
    private Coroutine _attackCoroutine;

    public GameObject worshiperObject;
    
    /// <summary>
    /// 다른 노드로 공격 보내기
    /// </summary>
    /// <param name="destination"></param>
    public void SendAttack(Node destination)
    {
        if (_attackCoroutine != null)
        {
            StopCoroutine(_attackCoroutine);
        }
        _attackCoroutine = StartCoroutine(SendWorshipers(destination));
    }

    private IEnumerator SendWorshipers(Node destination)
    {
        while (WorshipersToSend > 0)
        {
            // 숭배자 생성 및 초기화
            GameObject worshiper = Instantiate(worshiperObject, transform.position, Quaternion.identity);
            Worshiper worshiperComponent = worshiper.GetComponent<Worshiper>();
            worshiperComponent.Initialize(destination, Speed);

            // 숭배자 수 감소
            WorshipersToSend--;
            CurrentWorshipers--;
            
            // 잠시 대기 후 반복
            yield return new WaitForSeconds(0.5f); // 필요에 따라 시간 조정
        }
    }
}