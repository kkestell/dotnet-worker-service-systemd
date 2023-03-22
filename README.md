# dotnet-worker-service-systemd

.NET worker service example for systemd.

## Installation

### Raspberry Pi OS (32-bit)

```console
$ dotnet publish --runtime linux-arm --self-contained -c Release
$ rsync -a --delete ./bin/Release/net7.0/linux-arm/publish/ io.lan:/home/kyle/ExampleService
$ ssh io.lan chmod +x /home/kyle/ExampleService/ExampleService
$ ssh io.lan systemctl --user restart example.service
```

## Running as a systemd service

### Create Service

To create a systemd service, create a file called `example.service` in the `~/.config/systemd/user/` directory:

```console
$ mkdir -p ~/.config/systemd/user
$ nano ~/.config/systemd/user/example.service
```

In the file, add the following configuration:

```ini
[Unit]
Description=Example

[Service]
Type=simple
Restart=always
RestartSec=10
ExecStart=%h/ExampleService/ExampleService
WorkingDirectory=%h/ExampleService
KillMode=process

[Install]
WantedBy=default.target
```

### Enable and start

Enable and start the service:

```console
$ systemctl --user daemon-reload
$ systemctl --user enable example.service
$ systemctl --user start example.service
```

### Lingering

By default, systemd user instances are started when a user logs in and stopped when their last session ends. However, to ensure that Blink Comparator is always running, even when no user is logged in, you can enable "lingering" for the current user:

```console
$ loginctl enable-linger $(whoami)
```

### Check status

Check the status of the service:

```console
$ systemctl --user status example.service
```

### Logs

View the logs for the service:

```console
$ journalctl -f --user-unit example.service
```
