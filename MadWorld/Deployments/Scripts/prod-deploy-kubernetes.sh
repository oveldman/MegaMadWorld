#!/bin/bash
sudo docker image prune -f

sudo microk8s kubectl apply -f Deployments/Kubernetes/Environment/Config.yaml
sudo microk8s kubectl apply -f Deployments/Kubernetes/Environment/Secrets/Secrets-Prod.yaml
sudo microk8s kubectl apply -f Deployments/Kubernetes/Internal
sudo microk8s kubectl apply -f Deployments/Kubernetes/Environment/Ingress-Deployment-Prod.yaml
sudo microk8s kubectl apply -f Deployments/Kubernetes/External