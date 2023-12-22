#!/bin/bash
build_and_load_image () {
  docker build -f $1/Dockerfile -t madworld/vps/$2 .
  docker save madworld/vps/$2 > Deployment/Kubernetes/images/madworld-vps-$2.tar
  sudo microk8s images import < Deployment/Kubernetes/images/madworld-vps-$2.tar
  echo "$2 image loaded"
}

#This removes the error of nginx image not found while building the image
sudo docker pull nginx

mkdir -p Deployment/Kubernetes/images
sudo microk8s kubectl delete deployment,pods,services --all -n madworld
sudo microk8s kubectl delete deployment,pods,services --all -n default

build_and_load_image "MadWorld.Backend.API" "api"
build_and_load_image "MadWorld.Backend.Identity" "identity"
build_and_load_image "MadWorld.Frontend.Admin" "admin"
build_and_load_image "MadWorld.Frontend.UI" "ui"
build_and_load_image "MadWorld.ShipSimulator.API" "shipsimulatorbackend"
build_and_load_image "MadWorld.ShipSimulator.UI" "shipsimulatorfrontend"

sudo docker image prune -f