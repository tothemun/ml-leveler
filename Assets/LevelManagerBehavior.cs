using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManagerBehavior : MonoBehaviour {

    public List<Transform> _fingers = new List<Transform>();
    public float _level = 0;
    LineRenderer _line;
    float LineWidth = 0.1f;
    public Color UnLevel;
    public Color Level;
    public List<MeshRenderer> _aligns = new List<MeshRenderer>();
    public Transform CenterAlign;
    void Start() {
        _line = GetComponent<LineRenderer>();
    }


	void Update () {
        _level = _fingers[0].position.y - _fingers[1].position.y;
        bool IsLevel = Mathf.Abs(_level) < 0.0125f;
        _line.SetPosition(0, _fingers[0].position);
        _line.SetPosition(1, _fingers[1].position);
        _line.material.color = IsLevel? Level:UnLevel;
        foreach(MeshRenderer _m in _aligns) {
            _m.material.color = IsLevel ? Level : UnLevel;
        }
        LineWidth = Mathf.Lerp(LineWidth,IsLevel ? 0.0015f:0.0005f,Time.deltaTime / 0.0625f);
        _line.startWidth = LineWidth;
        _line.endWidth = LineWidth;

        transform.position = Vector3.Lerp(_fingers[0].position,_fingers[1].position,0.5f);
        transform.LookAt(_fingers[0].position);
        transform.Rotate(0,90,0);

        CenterAlign.localPosition = new Vector3(-_level,CenterAlign.transform.localPosition.y,0);
	}
}