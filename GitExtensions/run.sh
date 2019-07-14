#!/bin/zsh

# Debug:
# mono --debug --debugger-agent=transport=dt_socket,server=y,address=127.0.0.1:55555 bin/Debug//GitExtensions.exe
# mono --arch=32 --debug --debugger-agent=transport=dt_socket,server=y,address=127.0.0.1:55555 bin/Debug//GitExtensions.exe

case "$OSTYPE" in
    darwin*)    mono --arch=32 --debug bin/Debug/GitExtensions.exe;;
    linux*)     mono --debug bin/Debug/GitExtensions.exe;;
    msys*)    echo "WINDOWS" ;;
    *)        echo "unknown: $OSTYPE" ;;
esac
