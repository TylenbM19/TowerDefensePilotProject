using UnityEngine;

public class Bilder : MonoBehaviour
{
    private TowerDefense _selectedBulding = null;
    [SerializeField] private Camera _camera;

    private void Update()
    {
        if (Input.GetMouseButton(0))
            Metod();
    }

    public void SelectBulding(TowerDefense bulding)
    {
        _selectedBulding = bulding;
    }

    private void Metod()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        RaycastHit[] hits = Physics.RaycastAll(ray);

        foreach (RaycastHit hit in hits)
        {
            hit.transform.TryGetComponent<TowerPlace>(out TowerPlace towerPlace);
            towerPlace?.TryConstruct(_selectedBulding);
        }
    }
}
