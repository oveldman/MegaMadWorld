#!/bin/bash
sudo microk8s kubectl rollout restart -n default
sudo microk8s kubectl rollout restart -n madworld