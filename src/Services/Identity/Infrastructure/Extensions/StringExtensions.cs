using Microsoft.AspNetCore.Identity;

namespace AdventEcho.Identity.Infrastructure.Extensions;

public static class StringExtensions
{
    public static Dictionary<string, string[]> ToDictionary(this IEnumerable<IdentityError> errors)
    {
        return errors.ToDictionary(error => error.Code.ToFriendlyCode(), error => new[] { error.Description });
    }

    private static string ToFriendlyCode(this string code)
    {
        return code switch
        {
            "DuplicateUserName" => "User name already exists.",
            "DuplicateEmail" => "Email already exists.",
            "InvalidEmail" => "Email is invalid.",
            "InvalidUserName" => "User name is invalid.",
            "PasswordTooShort" => "Password must be at least 6 characters long.",
            "PasswordRequiresNonAlphanumeric" => "Password must contain at least one non-alphanumeric character.",
            "PasswordRequiresDigit" => "Password must contain at least one digit.",
            "PasswordRequiresLower" => "Password must contain at least one lowercase letter.",
            "PasswordRequiresUpper" => "Password must contain at least one uppercase letter.",
            "UserAlreadyHasPassword" => "User already has a password.",
            "UserLockoutNotEnabled" => "User lockout is not enabled.",
            "UserNotFound" => "User not found.",
            "UserAlreadyInRole" => "User is already in the role.",
            "UserNotInRole" => "User is not in the role.",
            "InvalidToken" => "Invalid token.",
            "InvalidOperation" => "Invalid operation.",
            "ConcurrencyFailure" => "Concurrency failure.",
            "DefaultError" => "An error occurred.", 
            _ => "An unknown error occurred."
        };
    }
}