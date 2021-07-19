usage() {
  echo -n "
      __        ____       ___       ____
     /  \      |    |   /  _  \    /  ___|
    / /\ \      |  |   |  | |  |  |  |
   /  __  \     |  |   |  | |  |  |  |
  /  /  \  \    |  |   |  |_|  |  |  |___
 /__/    \__\  |____|   \ ___ /    \ ____|

  SYNOPSIS
    aioc.sh COMMAND ARGS

  DESCRIPTION
    CLI utility for managing aioc development environment on local machines

  USAGE
    aioc.sh start   [SERVICE...]     Starts local environment with selected services
  "
}

start() {

}

stop() {
  echo "Stopping services"
}