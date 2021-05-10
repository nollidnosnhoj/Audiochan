import { HamburgerIcon, MoonIcon, SearchIcon, SunIcon } from "@chakra-ui/icons";
import {
  Box,
  Button,
  ButtonProps,
  Drawer,
  DrawerBody,
  DrawerCloseButton,
  DrawerContent,
  DrawerOverlay,
  Flex,
  Heading,
  HStack,
  IconButton,
  useColorMode,
  useColorModeValue,
  useDisclosure,
} from "@chakra-ui/react";
import React, { useRef } from "react";
import { FaCloudUploadAlt } from "react-icons/fa";
import { MdLibraryMusic } from "react-icons/md";
import NextLink from "next/link";
import Link from "../../Link";
import { useUser } from "~/lib/hooks/useUser";
import UserSection from "./UserSection";
import SearchBar from "./SearchBar";

interface HeaderMenuLinkProps extends ButtonProps {
  label: string;
  href: string;
  // eslint-disable-next-line @typescript-eslint/no-explicit-any
  icon?: React.ReactElement<any, string | React.JSXElementConstructor<any>>;
  hidden?: boolean;
}

const HeaderMenuLink = (props: HeaderMenuLinkProps) => {
  const { label, href, icon, hidden = false, ...buttonProps } = props;

  if (hidden) return null;

  return (
    <NextLink href={href}>
      <Button
        leftIcon={icon}
        width="100%"
        marginY={1}
        paddingY={6}
        paddingX={6}
        borderRadius={0}
        justifyContent="normal"
        variant="ghost"
        {...buttonProps}
      >
        {label}
      </Button>
    </NextLink>
  );
};

const Header: React.FC = () => {
  const { user } = useUser();
  const { toggleColorMode } = useColorMode();
  const headerColor = useColorModeValue("white", "gray.800");
  const ColorModeIcon = useColorModeValue(MoonIcon, SunIcon);
  // eslint-disable-next-line @typescript-eslint/no-explicit-any
  const menuButtonRef = useRef<any>();
  const {
    isOpen: isMenuDrawerOpen,
    onClose: onMenuDrawerClose,
    onToggle: onMenuDrawerToggle,
  } = useDisclosure();

  return (
    <React.Fragment>
      <Flex
        align="center"
        as="header"
        px={4}
        position="fixed"
        top={0}
        left={0}
        width="100%"
        boxShadow="md"
        bgColor={headerColor}
        zIndex={3}
      >
        <Flex
          width="100%"
          my={3}
          h={16}
          alignItems="center"
          justifyContent="space-between"
        >
          <Flex align="center" width={{ md: "full" }}>
            <IconButton
              variant="ghost"
              size="lg"
              icon={<HamburgerIcon />}
              aria-label="Open menu"
              onClick={onMenuDrawerToggle}
              position="relative"
              ref={menuButtonRef}
              isRound
            />
            <Box display={{ base: "none", md: "flex" }} marginLeft={14}>
              <Heading size="lg">
                <Link
                  href="/"
                  _hover={{
                    textDecoration: "none",
                  }}
                >
                  Audiochan
                </Link>
              </Heading>
            </Box>
          </Flex>
          <Flex marginX={8} justify="center" width="full">
            <Box width="100%">
              <SearchBar />
            </Box>
          </Flex>
          <HStack
            spacing={4}
            marginRight={{ base: 2, md: 8 }}
            width={{ md: "full" }}
            justifyContent="flex-end"
          >
            <IconButton
              aria-label="Change color mode"
              icon={<ColorModeIcon />}
              size="md"
              variant="ghost"
              onClick={toggleColorMode}
            />
            <UserSection />
          </HStack>
        </Flex>
      </Flex>
      <Drawer
        placement="left"
        onClose={onMenuDrawerClose}
        isOpen={isMenuDrawerOpen}
        initialFocusRef={menuButtonRef}
      >
        <DrawerOverlay>
          <DrawerContent>
            <DrawerCloseButton />
            <DrawerBody p={0} marginTop={12}>
              <HeaderMenuLink
                label="Browse"
                href="/audios"
                icon={<MdLibraryMusic />}
              />
              <HeaderMenuLink
                label="Search"
                href="/search"
                icon={<SearchIcon />}
              />
              <HeaderMenuLink
                label="Upload"
                href="/upload"
                icon={<FaCloudUploadAlt />}
                hidden={!user}
                variant="solid"
                colorScheme="primary"
              />
            </DrawerBody>
          </DrawerContent>
        </DrawerOverlay>
      </Drawer>
    </React.Fragment>
  );
};

export default Header;
