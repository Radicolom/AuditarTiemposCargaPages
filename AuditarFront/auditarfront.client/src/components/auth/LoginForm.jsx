import React, { useState } from 'react';
import { Login } from '../../llamadasApi/users';
import { useNavigate } from "react-router-dom";
import Fund from '../layout/fund';
import Swal from 'sweetalert2';

const LoginForm = () => {
    const [correo, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [loading, setLoading] = useState(false);
    const navigate = useNavigate();

    const handleSubmit = async (e) => {
        e.preventDefault();
        setLoading(true);
        const Result = await Login({ correo, password });
        setLoading(false);
        if (Result === true) {
            navigate("/");
        } else {
            Swal.fire({
                icon: 'error',
                title: 'Error',
                text: Result,
            });
        }
    };

    return (
        <div className="d-flex justify-content-center align-items-center min-vh-100 position-relative bg-gradient" style={{background: 'linear-gradient(135deg, #e0e7ff 0%, #f8fafc 100%)'}}>
            <Fund />
            <form onSubmit={handleSubmit} className="card shadow p-4 border-0 position-relative" style={{ minWidth: 340, maxWidth: 380, zIndex: 1 }}>
                <h2 className="mb-4 fw-bold text-primary text-center">Iniciar Sesión</h2>
                <div className="mb-3">
                    <label className="form-label">Correo electrónico</label>
                    <input
                        type="email"
                        name="email"
                        className="form-control form-control-lg"
                        required
                        value={correo}
                        onChange={e => setEmail(e.target.value)}
                        autoFocus
                        placeholder="usuario@dominio.com"
                    />
                </div>
                <div className="mb-3">
                    <label className="form-label">Contraseña</label>
                    <input
                        type="password"
                        name="password"
                        className="form-control form-control-lg"
                        required
                        value={password}
                        onChange={e => setPassword(e.target.value)}
                        placeholder="********"
                    />
                </div>
                <button
                    type="submit"
                    className="btn btn-primary w-100 fw-bold py-2 mt-2"
                    disabled={loading}
                >
                    {loading ? 'Ingresando...' : 'Ingresar'}
                </button>
            </form>
        </div>
    );
};

export default LoginForm;
