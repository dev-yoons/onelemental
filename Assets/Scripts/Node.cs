using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Onelemental.Enum;

public class Node : MonoBehaviour
{
    public int CurrentWorshipers { get; set; } = 30; 
    public float Speed { get; set; } = 10f;

    public const float LeastWorshipers = 10;

    public Elemental StartElemental = Elemental.Neutral;

    private Elemental CurrentElemental = Elemental.Neutral;
    public Elemental GetCurrentElemental() { return CurrentElemental; }

    public bool IsMainNode = false;
    
    private Coroutine _attackCoroutine;

    public GameObject worshiperObject;

    public SpriteRenderer NodeRenderer;

    public TextMesh NodeTextMesh;

    public List<Node> ConnectedNodes;

    /// <summary>
    /// 다른 노드로 공격 보내기
    /// </summary>
    /// <param name="destination"></param>
    /// 

    void Start()
    {
        SetCurrentElemental(StartElemental);
        NodeTextMesh.text = CurrentWorshipers.ToString();
    }

    public void SendAttack(GameObject destination)
    {
        if (_attackCoroutine != null)
        {
            StopCoroutine(_attackCoroutine);
        }
        _attackCoroutine = StartCoroutine(SendWorshipers(destination));
    }

    private void DecreaseWorshipers()
    {
        CurrentWorshipers--;
        NodeTextMesh.text = CurrentWorshipers.ToString(); 
    }

    private IEnumerator SendWorshipers(GameObject destination)
    {
        while (CurrentWorshipers > LeastWorshipers)
        {
            // 숭배자 생성 및 초기화
            GameObject worshiper = Instantiate(worshiperObject, transform.position, Quaternion.identity);
            Worshiper worshiperComponent = worshiper.GetComponent<Worshiper>();
            worshiperComponent.Initialize(destination, Speed);

            // 숭배자 수 감소 
            DecreaseWorshipers();
            
            // 잠시 대기 후 반복
            yield return new WaitForSeconds(0.5f); // 필요에 따라 시간 조정
        }
    }
    public void SetCurrentElemental(Elemental newElemental)
    {
        CurrentElemental = newElemental;

        // 아트 에셋 생기면 그 때 바꿉니다.
        switch(newElemental)
        {
            case Elemental.Fire:
                NodeRenderer.color = Color.red;
                break;
            case Elemental.Water:
                NodeRenderer.color = Color.blue;
                break;
            case Elemental.Wind:
                NodeRenderer.color = Color.gray;
                break;
            case Elemental.Ground:
                NodeRenderer.color = Color.yellow;
                break; 
        }
    }


}