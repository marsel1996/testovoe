import { Registry } from "../../api/TestApi";
import MyButton from "../Shared/MyButton";
import { Col, Modal, Row } from 'antd';
import { useEffect, useState } from "react";
import React from "react";
import { ModalComponent } from "../../models/ModalComponentProps";

type UserInfoProps = ModalComponent & {
    id: number,
}

type ColField = {
    name: string,
    value: string,
}

const UserInfo = ({id, onComplete}: UserInfoProps): JSX.Element => {
    const service: Registry.UserService = new Registry.UserService();
    
    const [fullName, setFullName] = useState<string>('');
    const [email, setEmail] = useState<string>('');
    const [phone, setPhone] = useState<string>('');
    const [roleName, setRoleName] = useState<string>('');

    const load = async () => {
        const info = await service.getUserInfo(id);
        setFullName(info?.fullName || '');
        setEmail(info?.email || '');
        setPhone(info?.phone || '');
        setRoleName(info?.roleName || '');
    }

    useEffect(() => {
        load();
    });

    const ColField = ({name, value}: ColField): JSX.Element => {
        return (
            <React.Fragment>
                <Col xs={12} lg={8}>{name}</Col>
                <Col xs={12} lg={16}>{value}</Col>
            </React.Fragment>
        )
    }
    
    return (
        <Modal key="modalInfo" title="Информация по пользователю" visible={true} 
            onCancel={() => onComplete(false)}
            footer={[
                <MyButton onClick={() => onComplete(false)}>
                    Закрыть
                </MyButton>
            ]}>
            <div>
                <Row>
                    <ColField name="Полное имя:" value={fullName}></ColField>
                </Row>
                <Row>
                    <ColField name="Почта:" value={email}></ColField>
                </Row>
                <Row>
                    <ColField name="Телефон:" value={phone}></ColField>
                </Row>
                <Row>
                    <ColField name="Роль:" value={roleName}></ColField>
                </Row>
            </div>
        </Modal>        
    );
}

export default UserInfo;