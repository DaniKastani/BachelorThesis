
from pynput.mouse import Listener

import win32api
import time

from pylsl import StreamInfo, StreamOutlet


def main():
    # the content-type to Markers, 1 channel, irregular sampling rate,
    # and string-valued data) The last value would be the locally unique
    # identifier for the stream as far as available, e.g.
    # program-scriptname-subjectnumber (you could also omit it but interrupted
    # connections wouldn't auto-recover). The important part is that the
    # content-type is set to 'Markers', because then other programs will know how
    #  to interpret the content

    info = StreamInfo('MyMarkerStream', 'Markers', 1, 0, 'string', 'myuidw43536')

    # next make an outlet
    outlet = StreamOutlet(info)

    print("now               sending makers")

    marker = "T"

    while True:
        # pick a sample to send an wait for a bit

        a = win32api.GetKeyState(0x9)
        if a < 0:
            outlet.push_sample(marker)
            print("click")
        time.sleep(0.2)


if __name__ == '__main__':
    main()
