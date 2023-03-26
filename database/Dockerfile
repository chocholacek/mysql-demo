FROM mysql:oracle

COPY ./my.cnf /etc/my.cnf
RUN chmod 644 /etc/my.cnf

COPY ./schema.sql /schema.sql
COPY ./data.sql /data.sql

RUN mysqld --initialize-insecure \
    && mkdir /mysql-files \
    && /bin/bash -c "mysqld --defaults-file=/etc/my.cnf &" \
    && sleep 5s \
    && echo "CREATE USER 'root'@'%' IDENTIFIED BY 'password'; GRANT ALL PRIVILEGES ON *.* TO 'root'@'%' WITH GRANT OPTION;" | mysql \
    && cat /schema.sql | mysql \
    && cat /data.sql | mysql \
    && mysqladmin shutdown

ENTRYPOINT ["mysqld", "--defaults-file=/etc/my.cnf"]
