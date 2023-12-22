#!/bin/bash
sudo microk8s kubectl -n default rollout restart deploy
sudo microk8s kubectl -n madworld rollout restart deploy