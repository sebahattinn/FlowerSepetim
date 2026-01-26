namespace CicekSepeti.Domain.Entities;

public class SiteSetting
{
    public int Id { get; set; }
    public bool IsMaintenanceMode { get; private set; } 

  
    public SiteSetting(bool isMaintenanceMode)
    {
        IsMaintenanceMode = isMaintenanceMode;
    }

  
    private SiteSetting() { }

   
    public void ChangeMaintenanceMode(bool status)
    {
       //kurallar buraya gelebilir ne bilim yılbaşı bakıma alınamaz gibisinden.
        IsMaintenanceMode = status;
    }
}