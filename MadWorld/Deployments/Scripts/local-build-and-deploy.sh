#!/bin/bash
#This removes the error of nginx image not found while building the image
sudo docker pull nginx

sudo kubectl delete deployment,pods,services --all -n madworld
sudo kubectl delete deployment,pods,services --all -n default

sudo docker image prune -f

minikube kubectl -- apply -f Deployments/Kubernetes/Environment/Config.yaml
minikube kubectl -- apply -f Deployments/Kubernetes/Environment/Secrets/Secrets-Local.yaml
minikube kubectl -- apply -f Deployments/Kubernetes/Internal
minikube kubectl -- apply -f Deployments/Kubernetes/Environment/Ingress-Deployment-Local.yaml
minikube kubectl -- apply -f Deployments/Kubernetes/External