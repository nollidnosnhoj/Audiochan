import { QueryKey } from "react-query";
import {
  useInfinitePagination,
  UseInfinitePaginationOptions,
  UseInfinitePaginationReturnType,
} from "~/lib/hooks";
import { AudioData } from "../types";
import { fetchAudioFeedHandler } from "../api";
import { useUser } from "~/features/user/hooks";

export const GET_AUDIO_FEED_QUERY_KEY: QueryKey = "feed";

export function useGetAudioFeed(
  options: UseInfinitePaginationOptions<AudioData> = {}
): UseInfinitePaginationReturnType<AudioData> {
  const { isLoggedIn } = useUser();
  return useInfinitePagination<AudioData>(
    GET_AUDIO_FEED_QUERY_KEY,
    (page) => fetchAudioFeedHandler(page),
    {
      enabled: isLoggedIn,
      ...options,
    }
  );
}
