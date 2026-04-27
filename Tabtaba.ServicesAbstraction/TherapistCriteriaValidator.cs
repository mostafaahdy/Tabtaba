using Tabtaba.Domain.Enums;

namespace Tabtaba.ServicesAbstraction;
public static class TherapistCriteriaValidator
{
   
    private static readonly Dictionary<TherapistCategory, List<DocumentType>> _requiredDocuments = new()
    {
        [TherapistCategory.Psychiatrist] = new()
        {
            DocumentType.GraduationCertificate,
            DocumentType.ProfessionalLicense,
            DocumentType.MedicalAssociationCertificate,
            DocumentType.PostgraduateDegree
        },
        [TherapistCategory.ClinicalPsychotherapist] = new()
        {
            DocumentType.MasterOfScienceOrArts,
            DocumentType.YearsOfClinicalExperience,
            DocumentType.ProofOfSupervision
        },
        [TherapistCategory.PsychologicalCounselor] = new()
        {
            DocumentType.MasterOfScienceOrArts,
            DocumentType.YearsOfCounselingExperience,
            DocumentType.ProofOfSupervision
        }
    };

   
    public static List<string> Validate(
        TherapistCategory category,
        List<DocumentType> submittedTypes)
    {
        var errors = new List<string>();

        if (!_requiredDocuments.TryGetValue(category, out var required))
        {
            errors.Add($"Unknown category: {category}");
            return errors;
        }

        var missing = required.Except(submittedTypes).ToList();
        foreach (var doc in missing)
            errors.Add($"Missing required document: {doc}");

        return errors;
    }

    
    public static List<DocumentType> GetRequiredDocuments(TherapistCategory category)
        => _requiredDocuments.TryGetValue(category, out var docs) ? docs : new();
}