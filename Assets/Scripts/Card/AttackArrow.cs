using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class AttackArrow : MonoBehaviour
{
    [SerializeField] private GameObject bodyPrefab;
    [SerializeField] private GameObject headPrefab;
    private const int AttackArrowPartsNumber = 17;
    private readonly List<GameObject> arrow = new List<GameObject>(AttackArrowPartsNumber);
    private Camera mainCamera;
    private bool isArrowEnabled;


    private void Start()
    {

        for (var i = 0; i < AttackArrowPartsNumber - 1; i++)
        {
            var body = Instantiate(bodyPrefab, gameObject.transform);
            arrow.Add(body);
            body.SetActive(true);
        }
        var head = Instantiate(headPrefab, gameObject.transform);
        arrow.Add(head);
        head.SetActive(true);
        mainCamera = Camera.main;
    }

    public void EnableArrow(Vector3 position)
    {
        gameObject.transform.position = position;
        gameObject.SetActive(true);
        isArrowEnabled = true;
    }

    public void UnableArrow()
    {
        gameObject.SetActive(false);
        isArrowEnabled = false;
    }

    private void LateUpdate()
    {
        if (!isArrowEnabled)
        {
            return;
        }
        var mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        var mouseX = mousePos.x;
        var mouseY = mousePos.y;
        const float centerX = -15.0f;
        const float centerY = -1.0f;
        //这几个参数用于计算贝塞尔曲线的控制点
        var controlAx = centerX - (mouseX - centerX) * 0.3f;
        var controlAy = centerY + (mouseY - centerY) * 0.8f;
        var controlBx = centerX + (mouseX - centerX) * 0.1f;
        var controlBy = centerY + (mouseY - centerY) * 1.4f;

        for (var i = 0; i < arrow.Count; i++)
        {
            var part = arrow[i];
            part.transform.position = new Vector3(centerX, y: centerY + 0.40f * i, z: 0.0f);
            //不同箭身部分根据索引值，得到对应位置的t值
            //越靠近箭尾部分，t值越小,
            // 越靠近箭头部分，t值越大
            var t = (i + 1) * 1.0f / arrow.Count;
            var tt = t * t;
            var ttt= tt * t;
            var u = 1.0f - t;
            var uu = u * u;
            var uuu = uu * u;

            var arrowX = uuu * centerX +3 * uu * t * controlAx +3 * u * tt * controlBx +ttt * mouseX;
            var arrowY = uuu * centerY +3 * uu * t * controlAy +3 * u * tt * controlBy +ttt * mouseY;
            arrow[i].transform.position = new Vector3(arrowX, arrowY, z: 0.0f);

            //计算箭身各部分精灵图片的朝向/角度
            float directionX;
            float directionY;
            if (i > 0)
            {
                //箭头部分的方向计算
                directionX = arrow[i].transform.position.x - arrow[i - 1].transform.position.x;
                directionY = arrow[i].transform.position.y - arrow[i - 1].transform.position.y;
            }
            else
            {
                //针对箭尾部分的方向计算
                directionX = arrow[i + 1].transform.position.x - arrow[i].transform.position.x;
                directionY = arrow[i + 1].transform.position.y - arrow[i].transform.position.y;
            }
            part.transform.rotation = Quaternion.Euler(0, 0, -Mathf.Atan2(directionX, directionY) * Mathf.Rad2Deg);
            part.transform.localScale = new Vector3( 1.0f - 0.03f * (arrow.Count - 1 - i),1.0f - 0.03f * (arrow.Count - 1 - i),0);
        }
        
    }
}
