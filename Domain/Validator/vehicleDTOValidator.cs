using MinimalApi.DTOs;

namespace MinimalApi.Domain.Validator;

public class VehicleDTOValidator
{
    public static List<string> Validate(VehicleDTO vehicleDTO)
    {
        var errors = new List<string>();

        if (string.IsNullOrEmpty(vehicleDTO.Name))
            errors.Add("Name is required.");

        if (string.IsNullOrEmpty(vehicleDTO.Marca))
            errors.Add("Marca is required.");

        if (vehicleDTO.Ano <= 0)
            errors.Add("Ano must be greater than 0.");

        return errors;
    }
}
