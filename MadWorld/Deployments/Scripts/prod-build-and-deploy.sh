#!/bin/bash
#This removes the error of nginx image not found while building the image
sudo docker pull nginx

mkdir -p Deployment/Kubernetes/images
sudo microk8s kubectl delete deployment,pods,services --all -n madworld
sudo microk8s kubectl delete deployment,pods,services --all -n default

sudo docker image prune -f

sudo microk8s kubectl apply -f Deployments/Kubernetes/Environment/Config.yaml
sudo microk8s kubectl apply -f Deployments/Kubernetes/Environment/Secrets/Secrets-Prod.yaml
sudo microk8s kubectl apply -f Deployments/Kubernetes/Internal
sudo microk8s kubectl apply -f Deployments/Kubernetes/Environment/Ingress-Deployment-Prod.yaml
sudo microk8s kubectl apply -f Deployments/Kubernetes/External

sudo rm -r Deployment/Kubernetes/images