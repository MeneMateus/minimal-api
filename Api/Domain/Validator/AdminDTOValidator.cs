using MinimalApi.Domain.DTOs;
using MinimalApi.Domain.Enums;

namespace MinimalApi.Domain.Validator;

public class AdminDTOValidator
{
    public static List<string> Validate(AdminDTO adminDTO)
    {
        var errors = new List<string>();
        if (string.IsNullOrEmpty(adminDTO.Name))
            errors.Add("Name must not be empty.");

        if (string.IsNullOrEmpty(adminDTO.Email))
            errors.Add("Email is required.");

        if (string.IsNullOrEmpty(adminDTO.Password))
            errors.Add("Password is required.");

        if (adminDTO.Profile == Profile.Admin || adminDTO.Profile == Profile.User)
            errors.Add("Profile must be selected.");

        return errors;
    }
}
