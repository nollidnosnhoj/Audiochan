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

interface ProgressBarProps {
  audioNode: HTMLAudioElement | null;
  currentTime: number;
  duration: number;
  onSeekChange: (time: number) => void;
}

const EMPTY_TIME_FORMAT = "--:--";

export default function ProgressBar(props: ProgressBarProps) {
  const { audioNode, duration, onSeekChange, currentTime } = props;
  const [sliderValue, setSliderValue] = useState(0);
  const [isDraggingProgress, setIsDraggingProgress] = useState(false);

  const formattedCurrentTime = useMemo(
    () => formatDuration(currentTime) ?? EMPTY_TIME_FORMAT,
    [currentTime]
  );

  const formattedDuration = useMemo(
    () => formatDuration(duration) ?? EMPTY_TIME_FORMAT,
    [duration]
  );

  const handleSliderChange = useCallback(
    (value: number) => {
      if (isDraggingProgress) {
        setSliderValue(value);
      }
    },
    [isDraggingProgress]
  );

  const handleSliderChangeStart = () => {
    setIsDraggingProgress(true);
  };

  const handleSliderChangeEnd = useCallback(
    (value: number) => {
      if (isDraggingProgress) {
        const time = Math.ceil(value);
        if (audioNode) audioNode.currentTime = time;
        setSliderValue(time);
        onSeekChange(time);
      }
      setIsDraggingProgress(false);
    },
    [isDraggingProgress]
  );

  const updateSeek = useCallback(
    _.throttle(() => {
      const time = Math.ceil(audioNode?.currentTime ?? 0);
      onSeekChange(time);
      if (!isDraggingProgress) {
        setSliderValue(time);
      }
    }, 200),
    [audioNode, isDraggingProgress]
  );

  useEffect(() => {
    audioNode?.addEventListener("timeupdate", updateSeek);

    return () => {
      audioNode?.removeEventListener("timeupdate", updateSeek);
    };
  }, [audioNode, updateSeek]);

  return (
    <Flex alignItems="center" width="100%">
      <Box fontSize="sm">{formattedCurrentTime}</Box>
      <Box flex="1" marginX={4}>
        <Slider
          min={0}
          max={duration}
          value={sliderValue}
          onChangeStart={handleSliderChangeStart}
          onChange={handleSliderChange}
          onChangeEnd={handleSliderChangeEnd}
          focusThumbOnChange={false}
          colorScheme="primary"
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