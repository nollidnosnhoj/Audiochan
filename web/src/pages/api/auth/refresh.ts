import type { NextApiRequest, NextApiResponse } from "next";
import api from "~/lib/api";
import {
  isAxiosError,
  getRefreshToken,
  setAccessTokenCookie,
  setRefreshTokenCookie,
} from "~/utils";

export default async (
  req: NextApiRequest,
  res: NextApiResponse
): Promise<void> => {
  try {
    if (req.method?.toUpperCase() !== "POST") {
      res.status(404).end();
      return;
    }

    const body = {
      refreshToken: getRefreshToken({ req }) || "",
    };

    const { status, data } = await api.post("auth/refresh", body, {
      skipAuthRefresh: true,
    });

    setAccessTokenCookie(data.accessToken, 60 * 60 * 24 * 7, { res });
    setRefreshTokenCookie(data.refreshToken, data.refreshTokenExpires, { res });
    res.status(status).json(data);
  } catch (err) {
    if (!isAxiosError(err)) {
      res.status(500).end();
    } else {
      setAccessTokenCookie("", 0, { res });
      setRefreshTokenCookie("", 0, { res });
      const status = err?.response?.status || 500;
      res.status(status).json(err?.response?.data);
    }
  }
};
