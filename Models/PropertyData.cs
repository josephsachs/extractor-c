namespace extractor_c.Models;

[System.Serializable]
public class PropertyData {
  public string covenant;
  public string covenant_effective_date;
  public string developer;
  public bool maximum_allowable_rent;
  public bool transferrable;
  public PropertyDescription legal_description;
}

[System.Serializable]
public class PropertyDescription {
  public string lot;
  public string plat;
  public string subdivision;
  public PropertyDescriptionBoundary[] boundaries;
}

[System.Serializable]
public class PropertyDescriptionBoundary {
  public string description;
}