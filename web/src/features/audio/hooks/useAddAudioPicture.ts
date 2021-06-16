import { useMutation, UseMutationResult, useQueryClient } from "react-query";
import { useUser } from "~/features/user/hooks";
import { ErrorResponse } from "~/lib/types";
import { AudioDetailData } from "../types";
import { GET_USER_AUDIOS_QUERY_KEY } from "~/features/user/hooks/useGetUserAudios";
import { GET_AUDIO_QUERY_KEY } from "./useGetAudio";
import { GET_AUDIO_LIST_QUERY_KEY } from "./useGetAudioList";
import { uploadAudioPictureHandler } from "../api";

export function useAddAudioPicture(
  id: string
): UseMutationResult<AudioDetailData, ErrorResponse, string> {
  const queryClient = useQueryClient();
  const [user] = useUser();
  const uploadArtwork = async (imageData: string): Promise<AudioDetailData> => {
    return await uploadAudioPictureHandler(id, imageData);
  };

  return useMutation(uploadArtwork, {
    onSuccess(data) {
      queryClient.setQueryData<AudioDetailData>(GET_AUDIO_QUERY_KEY(id), data);
      queryClient.invalidateQueries(GET_AUDIO_LIST_QUERY_KEY);
      if (user) {
        queryClient.invalidateQueries(GET_USER_AUDIOS_QUERY_KEY(user.id));
      }
    },
  });
}
