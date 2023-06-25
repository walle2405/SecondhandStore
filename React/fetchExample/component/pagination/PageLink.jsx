import { HTMLProps } from "react";
import cn from 'classnames';
import './PageLink.css';

export default function PageLink({ className,
    children,
    active,
    disabled,
    ...props
}) {
    const customClassName = cn('page-link', className, { active, disabled });

    if (disabled) {
        return <button type="button" disabled className={customClassName}>{children}</button>
    }
    return (
        <button type="button" {...props} className={customClassName} aria-current={active ? 'page' : undefined}>
            {children}
        </button>
    )
}
