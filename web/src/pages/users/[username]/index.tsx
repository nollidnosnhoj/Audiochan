import {
  Avatar,
  Box,
  Button,
  Flex,
  Tab,
  TabList,
  TabPanel,
  TabPanels,
  Tabs,
  Text,
} from "@chakra-ui/react";
import React from "react";
import { GetServerSideProps, InferGetServerSidePropsType } from "next";
import { useRouter } from "next/router";
import useSWR from "swr";
import Page from "~/components/Shared/Page";
import AudioList from "~/components/Audio/List";
import request from "~/lib/request";
import { ErrorResponse, Profile } from "~/lib/types";
import { useFollow } from "~/lib/services/users";
import { getAccessToken, getCookie } from "~/utils/cookies";
import CONSTANTS from "~/constants";

interface PageProps {
  initialData?: Profile;
}

export const getServerSideProps: GetServerSideProps<PageProps> = async (
  context
) => {
  const username = context.params?.username as string;
  const accessToken = getAccessToken(context);

  try {
    const { data: initialData } = await request<Profile>(`/users/${username}`, {
      accessToken: accessToken,
    });
    return {
      props: {
        initialData,
      },
    };
  } catch (err) {
    return {
      notFound: true,
    };
  }
};

export default function ProfilePage(
  props: InferGetServerSidePropsType<typeof getServerSideProps>
) {
  const { query } = useRouter();
  const username = query.username as string;
  const { data: profile } = useSWR<Profile, ErrorResponse>(
    `users/${username}`,
    {
      initialData: props.initialData,
    }
  );
  const { isFollowing, follow } = useFollow(username);

  return (
    <Page title={`${profile.username} | Audiochan`}>
      <Flex direction="row">
        <Box flex="1">
          <Box textAlign="center" marginBottom={4}>
            <Avatar size="2xl" name={profile.username} src="" />
            <br />
            <Text fontSize="lg" as="strong">
              {profile.username}
            </Text>
          </Box>
          <Flex justifyContent="center">
            <Button
              colorScheme="primary"
              variant={isFollowing ? "solid" : "outline"}
              disabled={isFollowing === undefined}
              paddingX={12}
              onClick={() => follow()}
            >
              {isFollowing ? "Followed" : "Follow"}
            </Button>
          </Flex>
        </Box>
        <Box flex="2">
          <Tabs isLazy>
            <TabList>
              <Tab>Uploads</Tab>
              <Tab>Favorites</Tab>
            </TabList>
            <TabPanels>
              <TabPanel>
                <AudioList type="user" username={profile.username} />
              </TabPanel>
              <TabPanel>
                <AudioList type="favorites" username={profile.username} />
              </TabPanel>
            </TabPanels>
          </Tabs>
        </Box>
      </Flex>
    </Page>
  );
}
