using UnityEngine;

public class Bilder : MonoBehaviour
{
    [SerializeField] private Camera _camera;

    private Tower _selectedBulding = null;

    private void Update()
    {
        if (Input.GetMouseButton(0))
            BuldingSiteSearch();
    }

    public void SelectBulding(Tower bulding)
    {
        _selectedBulding = bulding;
    }

    private void BuldingSiteSearch()
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
