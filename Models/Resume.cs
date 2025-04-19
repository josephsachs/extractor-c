namespace extractor_c.Models;

using System.Text.Json.Serialization;


[System.Serializable]
public class Resume {
  [JsonPropertyName("name")]
  public string? Name;
  [JsonPropertyName("address")]
  public string? Address;
  [JsonPropertyName("phone_numbers")]
  public string[]? PhoneNumbers;
  [JsonPropertyName("email")]
  public string? Email;
  [JsonPropertyName("websites")]
  public Website[]? Websites;
  [JsonPropertyName("would_relocate")]
  public Boolean? would_relocate;
  [JsonPropertyName("experience")]
  public Experience[] Experience;
  [JsonPropertyName("education")]
  public Education[] Education;
  [JsonPropertyName("portfolio")]
  public Portfolio[] Portfolio;
  [JsonPropertyName("skills")]
  public string[]? Skills;
}

[System.Serializable]
public class Experience {
  [JsonPropertyName("company")]
  public string? Company;
  [JsonPropertyName("title")]
  public string? Title;
  [JsonPropertyName("start_date")]
  public string? StartDate;
  [JsonPropertyName("end_date")]
  public string? EndDate;
  [JsonPropertyName("responsibilities")]
  public string[]? Responsibilities;
  [JsonPropertyName("office_type")]
  public string? OfficeType;
  [JsonPropertyName("is_manager")]
  public Boolean? IsManager;
  [JsonPropertyName("industry_classification")]
  public string? IndustryClassification;
}

[System.Serializable]
public class Education {
  [JsonPropertyName("institution")]
  public string? Institution;
  [JsonPropertyName("degree")]
  public string? Degree;
  [JsonPropertyName("date")]
  public string? Date;
  [JsonPropertyName("gpa")]
  public string? GPA;
  [JsonPropertyName("certification")]
  public string? Certification;
  [JsonPropertyName("expiration_date")]
  public string? ExpirationDate;
}


[System.Serializable]
public class Website {
  [JsonPropertyName("name")]
  public string? Name;
  [JsonPropertyName("url")]
  public string? Url;
}


[System.Serializable]
public class Portfolio {
  [JsonPropertyName("project_name")]
  public string? ProjectName;
  [JsonPropertyName("date")]
  public string? Date;
  [JsonPropertyName("url")]
  public string? Url;
  [JsonPropertyName("tools_used")]
  public string[]? ToolsUsed;
  [JsonPropertyName("is_professional")]
  public Boolean? isProfessional;
}