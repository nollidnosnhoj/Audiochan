import { Profile } from "~/features/user/types";
import api from "~/lib/api";

interface FetchUserProfileOptions {
  accessToken?: string;
}

export const fetchUserProfile = async (
  username: string,
  options: FetchUserProfileOptions = {}
): Promise<Profile> => {
  const { data } = await api.get<Profile>(`users/${username}`, undefined, {
    accessToken: options.accessToken,
  });

  return data;
};
