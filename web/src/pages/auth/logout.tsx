import { chakra } from "@chakra-ui/react";
import { useEffect } from "react";
import Router from "next/router";
import withRequiredAuth from "~/components/hoc/withRequiredAuth";
import Page from "~/components/Page";
import { useAuth } from "~/features/auth/hooks/useAuth";
import { toast } from "~/utils/toast";

const LogoutPage: React.FC = () => {
  const { logout } = useAuth();

  useEffect(() => {
    logout()
      .then(() => {
        Router.push("/").then(() => {
          toast("success", {
            title: "You have successfully logged out.",
          });
        });
      })
      .catch((err) => console.error(err));
  }, []);

  return (
    <Page title="Logging out">
      <chakra.span>Logging out...</chakra.span>
    </Page>
  );
};

export default withRequiredAuth(LogoutPage);
