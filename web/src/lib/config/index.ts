export default {
  BACKEND_API: process.env.NEXT_PUBLIC_BACKEND_API,
  FRONTEND_URL: process.env.NEXT_PUBLIC_FRONTEND_URL,
  IDENTITY: {
    usernameMinLength: 3,
    usernameMaxLength: 20,
    usernameAllowedChars: "abcdefghijklmnopqrstuvwxyz-_",
    passwordMinimumLength: 6,
    passwordRequiresDigit: true,
    passwordRequiresLowercase: true,
    passwordRequiresUppercase: true,
    passwordRequiresNonAlphanumeric: false,
  },
  UPLOAD: {
    AUDIO: {
      accept: ["audio/mp3", "audio/mpeg"],
      maxSize: 262144000,
    },
    IMAGE: {
      accept: ["image/jpeg", "image/png", "image/gif"],
      maxSize: 2097152,
    },
  },
};
