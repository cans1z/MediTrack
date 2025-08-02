'use client'
import useAuth from '@/hooks/useAuth';
import styles from './LoginScreen.module.css';
import { useEffect } from 'react';
import { useRouter } from 'next/navigation';
import {z} from 'zod';
import { useForm } from 'react-hook-form';
import { Label } from '@radix-ui/react-label';
import { Input } from '@/components/ui/input';
import { Button } from '@/components/ui/button';

const loginFormSchema = z.object({
  password: z.string().min(4, 'Минимум 4 символа'),
  userName: z.string()
});

const LoginScreen = () => {
  const auth = useAuth();
  const router = useRouter();

  useEffect(() => {
    if (!auth?.loading && !!auth?.user) {
      router.push('/');
      return;
    }
  }, [auth]);

  

  return (
    <div className='flex items-center justify-center h-screen'>
      <div className='border-1 bg-accent/45 rounded-2xl w-[400px] h-fit p-7.5 space-y-4'>
        <div className='w-full flex justify-center'>
          <div className='font-semibold text-xl'>Вход в систему</div>
        </div>

        <div className='space-y-2.5'>
          <div className='space-y-1.5'>
            <div className='opacity-85 text-sm font-medium'>Эл. Почта</div>
            <Input placeholder='Введите почту' />
          </div>

          <div className='space-y-1.5'>
            <div className='opacity-85 text-sm font-medium'>Пароль</div>
            <Input placeholder='Введите пароль' />
          </div>
        </div>
        
        <div className='flex justify-end w-full pt-2.5'>
          <Button>Далее</Button>
        </div>
      </div>
    </div>
  );
}

export default LoginScreen;