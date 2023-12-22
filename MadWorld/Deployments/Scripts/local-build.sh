#!/bin/bash
build_and_load_image () {
  sudo minikube image rm docker.io/madworld/vps/$2
  sudo docker build -f $1/Dockerfile -t madworld/vps/$2 .
  sudo minikube image load --overwrite madworld/vps/$2
  echo "$2 image loaded"
}

#This removes the error of nginx image not found while building the image
sudo docker pull nginx

sudo kubectl delete deployment,pods,services --all -n madworld
sudo kubectl delete deployment,pods,services --all -n default

build_and_load_image "MadWorld.Backend.API" "api"
build_and_load_image "MadWorld.Backend.Identity" "identity"
build_and_load_image "MadWorld.Frontend.Admin" "admin"
build_and_load_image "MadWorld.Frontend.UI" "ui"
build_and_load_image "MadWorld.ShipSimulator.API" "shipsimulatorbackend"
build_and_load_image "MadWorld.ShipSimulator.UI" "shipsimulatorfrontend"

sudo docker image prune -f