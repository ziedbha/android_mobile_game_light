using UnityEngine;

[ExecuteInEditMode]
public class Grid : MonoBehaviour {
	[SerializeField] Transform _transform;
	[SerializeField] Material _material;
	[SerializeField] Vector2 _gridSize;
	[SerializeField] int _rows;
	[SerializeField] int _columns;

	void Start() {
		UpdateGrid();
	}
		
	public void UpdateGrid() {
		_transform.localScale = new Vector3(_gridSize.x, _gridSize.y, 1.0f);
		_material.SetTextureScale("_MainTex", new Vector2( _columns, _rows));	
	}
	
}
