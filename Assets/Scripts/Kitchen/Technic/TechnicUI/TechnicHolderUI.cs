using UnityEngine;

public class TechnicHolderUI : MonoBehaviour
{
    [SerializeField] private Transform _targetPoint;
    [SerializeField] private TechnicStandardPanel _standardPanel;
    [SerializeField] private TechnicRepairPanel _repairPanel;
    private TechnicPanel _nowPanel;
    private TechnicHolder _nowTechnic;

    private void Start()
    {
        _standardPanel.ChangeState(false);
        _repairPanel.ChangeState(false);

        _repairPanel.RepairEnded += EndRepairTechnic;
        _nowPanel = _standardPanel;
    }

    private void Update()
    {
        if (_nowTechnic == null) return;

        _nowPanel.UpdatePanel();
    }

    public void OpenTechnic(TechnicHolder technic)
    {
        if (technic == _nowTechnic) {
            _nowPanel.ChangeState();
        } else {
            _nowPanel.ChangeState(false);
            if (technic.IsRepairing)
                _nowPanel = _repairPanel;
            else
                _nowPanel = _standardPanel;
            _nowPanel.ChangeState(true);
            _targetPoint.position = technic.UITarget.position;
            _nowTechnic = technic;
        }

        _nowPanel.SetNewTechnic(technic);
        _nowPanel.UpdateInfo();
    }

    public void StartRepairTechnic()
    {
        _nowTechnic.StartRepair();

        _nowPanel.ChangeState(false);
        _nowPanel = _repairPanel;
        _nowPanel.ChangeState(true);

        _nowPanel.SetNewTechnic(_nowTechnic);
        _nowPanel.UpdateInfo();
    }

    public void EndRepairTechnic()
    {
        _nowPanel.ChangeState(false);
        _nowPanel = _standardPanel;
        _nowPanel.ChangeState(true);

        _nowPanel.SetNewTechnic(_nowTechnic);
        _nowPanel.UpdateInfo();
    }
}
