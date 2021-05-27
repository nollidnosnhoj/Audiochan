import { string } from "yup";
import { DefinedStringSchema } from "yup/lib/string";
import { AnyObject } from "yup/lib/types";
import SETTINGS from "~/lib/config";
import { validationMessages } from "~/utils";

const {
  usernameMinLength,
  usernameMaxLength,
  usernameAllowedChars,
  passwordRequiresDigit: passwordRequireDigit,
  passwordRequiresLowercase: passwordRequireLowercase,
  passwordRequiresNonAlphanumeric: passwordRequireNonAlphanumeric,
  passwordRequiresUppercase: passwordRequireUppercase,
  passwordMinimumLength: passwordMinLength,
} = SETTINGS.IDENTITY;

export const usernameRule = (
  label: string
): DefinedStringSchema<string | undefined, Record<string, unknown>> => {
  let schema = string().test(
    "allowedCharacters",
    "Username can only contain lowercase, numbers, hyphens, or underscores.",
    (value) => {
      if (value) {
        for (const char of value) {
          if (usernameAllowedChars.indexOf(char) == -1) {
            return false;
          }
        }
      }
      return true;
    }
  );
  if (usernameMinLength)
    schema = schema.min(
      usernameMinLength,
      validationMessages.min(label, usernameMinLength)
    );
  if (usernameMaxLength)
    schema = schema.max(
      usernameMaxLength,
      validationMessages.max(label, usernameMaxLength)
    );
  return schema.defined();
};

export const passwordRule = (
  label: string
): DefinedStringSchema<string, AnyObject> => {
  let schema = string();
  if (passwordMinLength)
    schema = schema.min(
      passwordMinLength,
      validationMessages.min(label, passwordMinLength)
    );
  if (passwordRequireDigit)
    schema = schema.matches(/[\d]+/, "Password must contain one digit.");
  if (passwordRequireLowercase)
    schema = schema.matches(
      /[a-z]+/,
      "Password must contain one lowercase character."
    );
  if (passwordRequireUppercase)
    schema = schema.matches(
      /[A-Z]+/,
      "Password must contain one uppercase character."
    );
  if (passwordRequireNonAlphanumeric)
    schema = schema.matches(
      /[^a-zA-Z\d]+/,
      "Password must contain one non-alphanumeric character."
    );

  return schema.ensure().defined();
};
