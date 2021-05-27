import _ from "lodash";
import React, { useCallback, useEffect, useMemo, useState } from "react";
import {
  Box,
  Flex,
  Slider,
  SliderFilledTrack,
  SliderThumb,
  SliderTrack,
} from "@chakra-ui/react";
import { formatDuration } from "~/utils/format";
import { useAudioPlayer } from "~/features/audio/hooks/";

const EMPTY_TIME_FORMAT = "--:--";

export default function ProgressBar() {
  const { state, dispatch } = useAudioPlayer();
  const {
    audioRef,
    currentTime,
    currentAudio: currentPlaying,
    playIndex,
  } = state;
  const { duration } = currentPlaying || { duration: 0 };
  const [sliderValue, setSliderValue] = useState(0);
  const [isDraggingProgress, setIsDraggingProgress] = useState(false);

  const formattedCurrentTime = useMemo(() => {
    if (currentTime === undefined) return EMPTY_TIME_FORMAT;
    return formatDuration(currentTime);
  }, [currentTime]);

  const formattedDuration = useMemo(() => {
    if (duration === undefined) return EMPTY_TIME_FORMAT;
    return formatDuration(duration);
  }, [duration]);

  const handleSliderChange = useCallback(
    (value: number) => {
      console.log("slider change");
      if (isDraggingProgress) {
        setSliderValue(value);
      }
    },
    [isDraggingProgress]
  );

  const handleSliderChangeStart = () => {
    console.log("slider change start");
    setIsDraggingProgress(true);
  };

  const handleSliderChangeEnd = useCallback(
    (value: number) => {
      console.log("slider change end");
      if (isDraggingProgress) {
        if (audioRef) audioRef.currentTime = value;
        dispatch({ type: "SET_CURRENT_TIME", payload: value });
      }
      setIsDraggingProgress(false);
    },
    [audioRef, isDraggingProgress]
  );

  const updateSeek = useCallback(
    _.throttle(() => {
      const time = audioRef?.currentTime ?? (currentTime || 0);
      dispatch({ type: "SET_CURRENT_TIME", payload: time });
      if (!isDraggingProgress) {
        setSliderValue(time);
      }
    }, 200),
    [audioRef, isDraggingProgress]
  );

  useEffect(() => {
    audioRef?.addEventListener("timeupdate", updateSeek);

    return () => {
      audioRef?.removeEventListener("timeupdate", updateSeek);
    };
  }, [audioRef, updateSeek]);

  return (
    <Flex alignItems="center" width="100%">
      <Box fontSize="sm">{formattedCurrentTime}</Box>
      <Box flex="1" marginX={4}>
        <Slider
          colorScheme="primary"
          value={sliderValue}
          min={0}
          max={audioRef?.duration || duration || 100}
          step={0.5}
          onChangeStart={handleSliderChangeStart}
          onChangeEnd={handleSliderChangeEnd}
          onChange={handleSliderChange}
          focusThumbOnChange={false}
          isDisabled={playIndex === undefined}
        >
          <SliderTrack>
            <SliderFilledTrack />
          </SliderTrack>
          <SliderThumb />
        </Slider>
      </Box>
      <Box fontSize="sm">{formattedDuration}</Box>
    </Flex>
  );
}
