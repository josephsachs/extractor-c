namespace extractor_c.Models;

using System.Text.Json.Serialization;


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
  [JsonPropertyName("roles")]
  public string[]? Roles;
}

[System.Serializable]
public class Education {
  [JsonPropertyName("institution")]
  public string? Institution;
  [JsonPropertyName("degree")]
  public string? Degree;
  [JsonPropertyName("certification")]
  public string? Certification;
  [JsonPropertyName("date")]
  public string? Date;

}

[System.Serializable]
public class Resume {
  [JsonPropertyName("name")]
  public string? Name;
  [JsonPropertyName("address")]
  public string? Address;
  [JsonPropertyName("phone")]
  public string[]? Phone;
  [JsonPropertyName("email")]
  public string? Email;
  [JsonPropertyName("experience")]
  public Experience Experience;
  [JsonPropertyName("education")]
  public Education Education;
  [JsonPropertyName("skills")]
  public string[]? Skills;
}