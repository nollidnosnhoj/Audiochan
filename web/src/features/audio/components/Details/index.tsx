import {
  Badge,
  Box,
  Flex,
  Heading,
  IconButton,
  Spacer,
  Stack,
  Text,
  useColorModeValue,
  useDisclosure,
  VStack,
  Menu,
  MenuButton,
  MenuItem,
  MenuList,
  HStack,
} from "@chakra-ui/react";
import Router from "next/router";
import React, { useEffect } from "react";
import { EditIcon } from "@chakra-ui/icons";
import { MdQueueMusic } from "react-icons/md";
import { HiDotsVertical } from "react-icons/hi";
import AudioEditDrawer from "./AudioEditDrawer";
import AudioTags from "./AudioTags";
import Link from "~/components/ui/Link";
import { AudioDetailData, Visibility } from "~/features/audio/types";
import { useUser } from "~/features/user/hooks";
import { relativeDate } from "~/utils/time";
import { mapAudioForAudioQueue } from "~/utils/audioplayer";
import AudioPlayButton from "../AudioPlayButton";
import { useAudioQueue } from "~/lib/stores";
import AudioFavoriteButton from "./AudioFavoriteButton";
import PictureController from "~/components/Picture";
import { useAddAudioPicture } from "../../hooks";
import AudioShareModal from "./AudioShareModal";
import { FaShare } from "react-icons/fa";

interface AudioDetailProps {
  audio: AudioDetailData;
}

const AudioDetails: React.FC<AudioDetailProps> = ({ audio }) => {
  const secondaryColor = useColorModeValue("black.300", "gray.300");
  const { user: currentUser } = useUser();
  const addToQueue = useAudioQueue((state) => state.addToQueue);

  const {
    isOpen: isEditOpen,
    onOpen: onEditOpen,
    onClose: onEditClose,
  } = useDisclosure();

  const {
    isOpen: isShareOpen,
    onOpen: onShareOpen,
    onClose: onShareClose,
  } = useDisclosure();

  const { mutateAsync: addPictureAsync, isLoading: isAddingPicture } =
    useAddAudioPicture(audio.id);

  useEffect(() => {
    Router.prefetch(`/users/${audio.user.username}`);
  }, []);

  return (
    <>
      <Flex marginBottom={4} justifyContent="center">
        <Box flex="1" marginRight={4}>
          <PictureController
            title={audio.title}
            src={audio.picture || ""}
            onChange={async (croppedData) => {
              await addPictureAsync(croppedData);
            }}
            isUploading={isAddingPicture}
            canEdit={currentUser?.id === audio.user.id}
          />
        </Box>
        <Box flex="5">
          <Stack direction="row" marginBottom={4}>
            <AudioPlayButton audio={audio} />
            <Stack direction="column" spacing="0" fontSize="sm">
              <Link href={`/users/${audio.user.username}`}>
                <Text fontWeight="500">{audio.user.username}</Text>
              </Link>
              <Text color={secondaryColor}>{relativeDate(audio.created)}</Text>
            </Stack>
            <Spacer />
            <HStack>
              <IconButton
                aria-label="Share audio"
                icon={<FaShare />}
                colorScheme="primary"
                variant="ghost"
                isRound
                onClick={onShareOpen}
                visibility={
                  audio.visibility == Visibility.Private ? "hidden" : "visible"
                }
              />
              <AudioFavoriteButton audioId={audio.id} />
              <Menu placement="bottom-end">
                <MenuButton
                  as={IconButton}
                  icon={<HiDotsVertical />}
                  variant="ghost"
                  isRound
                />
                <MenuList>
                  {audio.user.id === currentUser?.id && (
                    <MenuItem icon={<EditIcon />} onClick={onEditOpen}>
                      Edit
                    </MenuItem>
                  )}
                  <MenuItem
                    icon={<MdQueueMusic />}
                    onClick={() => addToQueue(mapAudioForAudioQueue(audio))}
                  >
                    Add to queue
                  </MenuItem>
                </MenuList>
              </Menu>
            </HStack>
          </Stack>
          <Stack direction="column" spacing={2} width="100%">
            <Flex as="header">
              <Box>
                <Flex alignItems="center">
                  <Heading as="h1" fontSize="2xl">
                    {audio.title}
                  </Heading>
                </Flex>
              </Box>
              <Spacer />
              <VStack spacing={2} alignItems="normal" textAlign="right">
                {audio.visibility === Visibility.Private && (
                  <Badge>PRIVATE</Badge>
                )}
              </VStack>
            </Flex>
            <AudioTags tags={audio.tags} />
          </Stack>
        </Box>
      </Flex>
      <AudioEditDrawer
        audio={audio}
        isOpen={isEditOpen}
        onClose={onEditClose}
      />
      <AudioShareModal
        audioId={audio.id}
        isOpen={isShareOpen}
        onClose={onShareClose}
      />
    </>
  );
};

export default AudioDetails;
